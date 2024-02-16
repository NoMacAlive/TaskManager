import React, {useCallback, useState} from 'react';
import {Formik, Field, Form, FormikHelpers} from 'formik';
import styled from "styled-components";
import {EditIcon} from "@/app/EditIcon";
import {DeleteIcon} from "@/app/DeleteIcon";
import {Table, TableBody, TableCell, TableColumn, TableHeader, TableRow} from "@nextui-org/table";
import {useDisclosure} from '@nextui-org/modal';
import {EditTaskModal} from './EditTaskModal';
import {DeleteTaskModal} from "@/components/DeleteTaskModal";

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
}


const TaskList: React.FC<TaskListProps> = ({tasks}) => {
    const editModal = useDisclosure();
    const deleteModal = useDisclosure();
    const [editingTask, setEditingTask] = useState<Task>({
        id: 0,
        title: "",
        description: "",
        dueDate: new Date(),
        priority: 0,
        status: 0
    })

    const [deleteTask, setDeleteTask] = useState<Task>({
        id: 0,
        title: "",
        description: "",
        dueDate: new Date(),
        priority: 0,
        status: 0
    })


    const onEditPressed = useCallback(
        (task: Task) => {
            setEditingTask(task);
            editModal.onOpen();
        },
        [],
    )

    const onDeletePressed = useCallback(
        (task: Task) => {
            setDeleteTask(task);
            deleteModal.onOpen();
        },
        [],)

    const columns = [
        {uid: "title", name: "Title"},
        {uid: "description", name: "Description"},
        {uid: "dueDate", name: "Due Date"},
        {uid: "priority", name: "Priority"},
        {uid: "status", name: "Status"},
        {uid: "actions", name: "Actions"}
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
                            <EditIcon onClick={() => onEditPressed(task)}/>
                        </span>

                        <span className="text-lg text-danger cursor-pointer active:opacity-50">
                            <DeleteIcon onClick={() => onDeletePressed(task)}/>
                        </span>
                    </div>
                );
            default:
                return <div></div>;
        }
    }, []);

    return (
        <div>
            <EditTaskModal isOpen={editModal.isOpen} onOpen={editModal.onOpen} onOpenChange={editModal.onOpenChange}
                           initialValues={{
                               id: editingTask.id,
                               title: editingTask.title,
                               description: editingTask.description,
                               dueDate: editingTask.dueDate.toISOString(),
                               priority: editingTask.priority,
                               status: editingTask.status,
                           }}/>

            <DeleteTaskModal isOpen={deleteModal.isOpen} onOpen={deleteModal.onOpen}
                             onOpenChange={deleteModal.onOpenChange} id={deleteTask.id} title={deleteTask?.title}/>

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
        </div>

    );
};

export default TaskList;
