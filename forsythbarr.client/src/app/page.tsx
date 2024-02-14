'use client'

import styled from 'styled-components'

const Title = styled.h1`
    font-size: 2.5rem;
    font-weight: bold;
    color: #333;
    text-align: center;
    margin-bottom: 1rem;
`
const Home = () => {
    return (
        <div className={"flex flex-col"}>
          <Title>Task Manager</Title>
        </div>
    );
};

export default Home;
