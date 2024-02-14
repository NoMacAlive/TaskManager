'use client'

import styled from 'styled-components'
import {useCallback, useEffect, useState} from 'react'

const Title = styled.h1`
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
    text-align: center;
    margin-bottom: 1rem;
`
const Page = () => {
    const [tasks, setTasks] = useState([])
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

    return (
        <div className={"flex flex-col"}>
            <Title>Task Manager</Title>
        </div>
    );
};

export default Page;
