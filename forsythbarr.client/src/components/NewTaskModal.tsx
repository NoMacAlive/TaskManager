'use client'
import { Button } from "@nextui-org/button";
import { Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Input } from "@nextui-org/react";
import * as Yup from 'yup';

import React from 'react';
import { useFormik } from "formik";

interface NewTaskModalProps {
    isOpen: boolean;
    onOpen: () => void;
    onOpenChange: () => void;
}

interface InitialValues {
    title: string;
    description: string;
    dueDate: string;
    priority: number;
    status: number;
}


const initialValues: InitialValues = {
    title: '',
    description: '',
    dueDate: '',
    priority: 0,
    status: 0,
};

const validationSchema = Yup.object({
    title: Yup.string().required('Title is required'),
    description: Yup.string().required('Description is required'),
    dueDate: Yup.date().required('Due date is required'),
    priority: Yup.number().min(0, 'Priority must be at least 0'),
    status: Yup.number().min(0).max(2, 'Status must be between 0 and 2'),
});

export const NewTaskModal: React.FC<NewTaskModalProps> = ({ isOpen, onOpen, onOpenChange }) => {
    const formik = useFormik({
        initialValues: initialValues,
        validationSchema: validationSchema,
        onSubmit: async (values) => {
            await fetch("http://localhost:5095/Task", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values),
            })


        },
    });
    return (
        <Modal
            isOpen={isOpen}
            onOpenChange={onOpenChange}
            placement="top-center"
        >
            <ModalContent>
                {(onClose) => (
                    <>
                        <form onSubmit={() => {
                            formik.handleSubmit()
                            onClose()
                        }}>
                            <ModalHeader className="flex flex-col gap-1">New Task</ModalHeader>
                            <ModalBody>
                                <Input
                                    autoFocus
                                    label="Title"
                                    placeholder="Task Title"
                                    variant="bordered"
                                    name="title"
                                    value={formik.values.title}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    errorMessage={formik.touched.title && Boolean(formik.errors.title)}
                                    description={formik.touched.title && formik.errors.title}
                                />
                                <Input
                                    label="Description"
                                    placeholder="Task Description"
                                    variant="bordered"
                                    name="description"
                                    value={formik.values.description}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    errorMessage={formik.touched.description && Boolean(formik.errors.description)}
                                    description={formik.touched.description && formik.errors.description}
                                />
                                <Input
                                    label="Due Date"
                                    placeholder="Task Due Date"
                                    variant="bordered"
                                    type="date"
                                    name="dueDate"
                                    value={formik.values.dueDate}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    errorMessage={formik.touched.dueDate && Boolean(formik.errors.dueDate)}
                                    description={formik.touched.dueDate && formik.errors.dueDate}
                                />
                                <Input
                                    label="Priority"
                                    placeholder="Task Priority"
                                    variant="bordered"
                                    type="number"
                                    name="priority"
                                    value={formik.values.priority.toString()}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    errorMessage={formik.touched.priority && Boolean(formik.errors.priority)}
                                    description={formik.touched.priority && formik.errors.priority}
                                />
                                <Input
                                    label="Status"
                                    placeholder="Task Status"
                                    variant="bordered"
                                    type="number"
                                    name="status"
                                    value={formik.values.status.toString()}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    errorMessage={formik.touched.status && Boolean(formik.errors.status)}
                                    description={formik.touched.status && formik.errors.status}
                                />

                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="flat" onPress={onClose}>
                                    Close
                                </Button>
                                <Button color="primary" type="submit" disabled={formik.isSubmitting}>
                                    Save
                                </Button>
                            </ModalFooter>
                        </form>
                    </>
                )}
            </ModalContent>
        </Modal>
    )
}
