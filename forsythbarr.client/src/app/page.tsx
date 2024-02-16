'use client'

import styled from 'styled-components'
import { useCallback, useEffect, useState } from 'react'
import { Table } from "@nextui-org/table";
import TaskList from "@/components/TaskList";
import { Button } from '@nextui-org/button';
import { NextUIProvider, useDisclosure } from '@nextui-org/react';
import { NewTaskModal } from '@/components/NewTaskModal';

const Container = styled.div`
    padding: 16px;
    border-radius: 4px;
`

const Title = styled.h1`
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
    text-align: center;
    margin-bottom: 1rem;
`

interface Task {
    "id": number
    "title": string
    "description": string
    "dueDate": Date
    "priority": number
    "status": number
}

const Page = () => {
    const [tasks, setTasks] = useState<Task[]>([])
    const { isOpen, onOpen, onOpenChange } = useDisclosure();
    const getTasks = useCallback(async () => {
        fetch("http://localhost:5095/Task")
            .then(response => response.json())
            .then(data => {
                const tasks: Task[] = data.map((task: any) => ({
                    id: task.id,
                    title: task.title,
                    description: task.description,
                    dueDate: new Date(task.dueDate),
                    priority: task.priority,
                    status: task.status
                }));
                setTasks(tasks)
            })
            .catch(error => console.error('Error fetching tasks:', error));
    }, []);

    useEffect(() => {
        getTasks()
    }, []);


    return (
        <NextUIProvider>
            <Container>
                <Title>Task Manager by Guangya Zhu</Title>
                <NewTaskModal isOpen={isOpen} onOpen={onOpen} onOpenChange={onOpenChange}></NewTaskModal>
                <Button color={"primary"} onPress={onOpen}>New Task</Button>
                <TaskList tasks={tasks}></TaskList>
            </Container>
        </NextUIProvider>

    );

};

export default Page;
