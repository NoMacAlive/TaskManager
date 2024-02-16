import React, { useCallback, useState } from 'react';
import { Formik, Field, Form, FormikHelpers } from 'formik';
import styled from "styled-components";
import { EditIcon } from "@/app/EditIcon";
import { DeleteIcon } from "@/app/DeleteIcon";
import { Table, TableBody, TableCell, TableColumn, TableHeader, TableRow } from "@nextui-org/table";

interface Task {
    id: number;
    title: string;
    description: string;
    dueDate: Date;
    priority: number;
    status: number;
}

enum Status {
    Pending,
    InProgress,
    Completed,
}

interface TaskListProps {
    tasks: Task[];
    onDelete: (id: number) => void;
    onUpdate: (index: number, task: Task) => void;
}


const TaskList: React.FC<TaskListProps> = ({ tasks, onDelete, onUpdate }) => {
    console.log(tasks)
    const [editIndex, setEditIndex] = useState<number | null>(null);

    const handleEdit = (index: number) => {
        setEditIndex(index);
    };

    const handleCancelEdit = () => {
        setEditIndex(null);
    };

    const columns = [
        { uid: "title", name: "Title" },
        { uid: "description", name: "Description" },
        { uid: "dueDate", name: "Due Date" },
        { uid: "priority", name: "Priority" },
        { uid: "status", name: "Status" },
        { uid: "actions", name: "Actions" }
    ];

    const renderCell = useCallback((task: Task, columnKey: React.Key) => {
        const cellValue = task[columnKey as keyof Task];

        switch (columnKey.toString()) {
            case "title":
                return (
                    <div className="text-bold">{cellValue.toString()}</div>
                );
            case "description":
                return (
                    <div>{cellValue.toString()}</div>
                );
            case "dueDate":
                return (
                    <div>{new Date(cellValue).toLocaleString()}</div>
                );
            case "priority":
                return (
                    <div>{cellValue.toString()}</div>
                );
            case "status":
                return (
                    <div className="capitalize">{Status[cellValue as number]}</div>
                );
            case "actions":
                return (
                    <div className="relative flex items-center gap-2">
                        <span className="text-lg text-default-400 cursor-pointer active:opacity-50">
                            <EditIcon />
                        </span>

                        <span className="text-lg text-danger cursor-pointer active:opacity-50">
                            <DeleteIcon />
                        </span>
                    </div>
                );
            default:
                return <div></div>;
        }
    }, []);

    const renderForm = (task: Task, index: number) =>
    (
        <Formik className={"flex flex-row"}
            initialValues={task}
            onSubmit={(values: Task, actions: FormikHelpers<Task>) => {
                onUpdate(index, values);
                setEditIndex(null);
                actions.setSubmitting(false);
            }}
        >
            {({ handleSubmit }) => (
                <Form className={"flex flex-row"}>
                    <Field type="text" name={`title`} />
                    <Field type="text" name={`description`} />
                    <Field type="date" name={`dueDate`} />
                    <Field type="number" name={`priority`} />
                    <Field type="number" name={`status`} />
                    <button type="submit">Submit</button>
                    <button type="button" onClick={handleCancelEdit}>
                        Cancel
                    </button>
                </Form>
            )}
        </Formik>
    )

    return (
        <Table
            className={"flex flex-col items-center border-solid border-2 border-indigo-600 justify-center bg-slate-200"}>
            <TableHeader columns={columns}>
                {(column) => (
                    <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                        {column.name}
                    </TableColumn>
                )}
            </TableHeader>
            <TableBody items={tasks}>
                {tasks.map((task, index) => {
                    return (
                        <TableRow key={task.id}>
                            {(columnKey) => <TableCell>{renderCell(task, columnKey)}</TableCell>}
                        </TableRow>
                    )
                }
                )}
            </TableBody>

        </Table>
    );
};

export default TaskList;
