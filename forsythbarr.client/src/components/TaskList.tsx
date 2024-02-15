import React, { useState } from 'react';
import { Formik, Field, Form, FormikHelpers } from 'formik';
import styled from "styled-components";
import {EditIcon} from "@/app/EditIcon";
import {DeleteIcon} from "@/app/DeleteIcon";

interface Task {
    id: number;
    title: string;
    description: string;
    dueDate: Date;
    priority: number;
    status: number;
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

    return (
        <div className={"flex flex-col items-center border-solid border-2 border-indigo-600"}>
            {tasks.map((task, index) => (
                <div key={task.id} className={"flex flex-row"}>
                    {editIndex === index ? (
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
                    ) : (
                        <div className={"flex flex-row gap-3.5 hover:bg-amber-100"}>
                            <div>Title: {task.title}</div>
                            <div>Description: {task.description}</div>
                            <div>Due Date: {task.dueDate.toLocaleDateString()}</div>
                            <div>Priority: {task.priority}</div>
                            <div>Status: {task.status}</div>
                            <button onClick={() => handleEdit(index)}><EditIcon></EditIcon></button>
                            <button onClick={() => onDelete(task.id)}><DeleteIcon></DeleteIcon></button>
                        </div>
                    )}
                </div>
            ))}
        </div>
    );
};

export default TaskList;
