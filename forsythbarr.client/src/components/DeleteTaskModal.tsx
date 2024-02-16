import React, { useCallback } from "react";
import { Modal, ModalBody, ModalContent, ModalFooter, ModalHeader } from "@nextui-org/react";
import { Button } from "@nextui-org/button";

interface DeleteTaskModalProps {
    id: number;
    title: string;
    isOpen: boolean;
    onOpen: () => void;
    onOpenChange: () => void;
}

export const DeleteTaskModal: React.FC<DeleteTaskModalProps> = ({ id, title, isOpen, onOpen, onOpenChange }) => {
    const onDelete = useCallback(async () => {
        await fetch("http://localhost:5095/Task/" + id, {
            method: "DELETE",
        })
    }, [id]);

    return (
        <Modal isOpen={isOpen} onOpenChange={onOpenChange}>
            <ModalContent>
                {(onClose) => (
                    <>
                        <ModalHeader className="flex flex-col gap-1">Delete Task</ModalHeader>
                        <ModalBody>
                            <p>
                                Are you sure about deleting {title}?
                            </p>
                        </ModalBody>
                        <ModalFooter>
                            <Button color="danger" variant="light" onPress={onClose}>
                                Close
                            </Button>
                            <Button color="primary" onPress={() => {
                                onDelete()
                                onClose()
                            }}>
                                Confirm
                            </Button>
                        </ModalFooter>
                    </>
                )}
            </ModalContent>
        </Modal>
    )
}