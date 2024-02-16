import React from 'react';
import {render, fireEvent} from '@testing-library/react';
import {NewTaskModal} from './NewTaskModal';

const handleSubmit = jest.fn();
const handleBlur = jest.fn();
const handleChange = jest.fn();

jest.mock('formik', () => ({
    useFormik: () => ({
        handleSubmit,
        initialValues: {
            title: '',
            description: '',
            dueDate: '',
            priority: 0,
            status: 0,
        },
        values: {
            title: 'Title',
            description: '',
            dueDate: '',
            priority: 0,
            status: 0,
        },
        validationSchema: {},
        handleBlur: jest.fn(),
        handleChange: jest.fn(),
        errors: {},
        touched: {}
    }),
}));

describe('NewTaskModal', () => {
    it('submits form data correctly', () => {
        const props = {
            isOpen: true,
            onOpen: jest.fn(),
            onOpenChange: jest.fn(),
        };

        const {getByTestId} = render(<NewTaskModal {...props} />);

        fireEvent.submit(getByTestId('task-form'));

        expect(handleSubmit).toHaveBeenCalled();
    });
});
