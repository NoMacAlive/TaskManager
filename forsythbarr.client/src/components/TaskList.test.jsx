import React from 'react';
import { render } from '@testing-library/react';
import TaskList from './TaskList';

jest.mock('@nextui-org/modal', () => ({
    useDisclosure: jest.fn(() => ({
        isOpen: false,
        onOpen: jest.fn(),
        onOpenChange: jest.fn(),
    })),
}));

jest.mock('./EditTaskModal', () => ({
    EditTaskModal: jest.fn(() => <div>EditTaskModal</div>),
}));

jest.mock('./DeleteTaskModal', () => ({
    DeleteTaskModal: jest.fn(() => <div>DeleteTaskModal</div>),
}));

describe('TaskList', () => {
    const tasks = [
        {
            id: 1,
            title: 'Task 1',
            description: 'Description 1',
            dueDate: new Date(),
            priority: 1,
            status: 0,
        },
        {
            id: 2,
            title: 'Task 2',
            description: 'Description 2',
            dueDate: new Date(),
            priority: 2,
            status: 1,
        },
    ];

    it('renders table with tasks', () => {
        const { getByText } = render(<TaskList tasks={tasks} />);
        expect(getByText('Task 1')).toBeInTheDocument();
        expect(getByText('Task 2')).toBeInTheDocument();
    });
});
