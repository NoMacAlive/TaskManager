'use client'

import styled from 'styled-components'
import {useCallback, useEffect, useState} from 'react'
import {Table, TableHeader, TableBody, TableColumn, TableRow, TableCell} from "@nextui-org/table";
import {EditIcon} from "@/app/EditIcon";
import {DeleteIcon} from "@/app/DeleteIcon";

const Container = styled.div`
    padding: 16px;
    border: 1px solid #ddd;
    border-radius: 4px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
`

const Title = styled.h1`
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
    text-align: center;
    margin-bottom: 1rem;
`

const StyledTable = styled(Table)`
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
    const getTasks = useCallback(async () => {
        const res = await fetch("http://localhost:5095/Task");

        if (!res.ok) {
            throw new Error('Failed to fetch data')
        }

        setTasks(await res.json())
    }, []);

    useEffect(() => {
        getTasks()
    }, []);

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
                    <div className="capitalize">{cellValue.toString()}</div>
                );
            case "actions":
                return (
                    <div className="relative flex items-center gap-2">
                        <span className="text-lg text-default-400 cursor-pointer active:opacity-50">
                            <EditIcon/>
                        </span>
                        
                        <span className="text-lg text-danger cursor-pointer active:opacity-50">
                            <DeleteIcon/>
                        </span>
                    </div>
                );
            default:
                return <div></div>;
        }
    }, []);

    const columns = [
        {uid: "title", name: "Title"},
        {uid: "description", name: "Description"},
        {uid: "dueDate", name: "Due Date"},
        {uid: "priority", name: "Priority"},
        {uid: "status", name: "Status"},
        {uid: "actions", name: "Actions"}
    ];

    return (
        <Container className={"content-center"}>
            <Title>Task Manager</Title>
            <StyledTable className={"justify-center bg-slate-200"}>
                <TableHeader columns={columns}>
                    {(column) => (
                        <TableColumn key={column.uid} align={column.uid === "actions" ? "center" : "start"}>
                            {column.name}
                        </TableColumn>
                    )}
                </TableHeader>
                <TableBody items={tasks}>
                    {(task) => (
                        <TableRow key={task.id}>
                            {(columnKey) => <TableCell className={"px-6"}>{renderCell(task, columnKey)}</TableCell>}
                        </TableRow>
                    )}
                </TableBody>
            </StyledTable>
        </Container>
        
    );

};

export default Page;
