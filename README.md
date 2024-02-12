## Forysth Barr Take Home Exercise
This exercise requires you to create a full-stack application using React, .NET 8, and a Database of your choice. 

When completing this exercise, please make sure everything is production quality and make sure to follow best software development practice to the best of your ability.

### The Exercise

You have been approached by a company to make an application to manage tasks.

#### Key features:
- Users can create, read, update, and delete tasks. Each task should have the following attributes: 
  - Title
  - Description
  - Due date
  - Priority
  - Status (e.g., pending, in progress, completed).
- Users can filter and sort tasks based on status, priority, and due date.
- Events must be generated for the following task changes:
  - Task creation
  - Task status change
  - Task due date change
  - Task priority change

#### Instructions

##### Front-end:

Use React to create a user-friendly UI for managing tasks. Implement components for task listing, task creation/editing forms, and navigation.

Feel free to use any libraries or packages to help you in development. E.g. React-Boostrap, NextJS, etc. You have full artistic control.

##### Back-end:

Develop an API using .NET 8. Implement endpoints for task management as required.

Feel free to use any libraries or packages to help you in development. E.g. Entity Framework Core, Dapper, Fluent Validation, etc.

Given that one of the requirements is to generate events for task updates, we have created an interface for you to implement so that you can print a message when the event is handled. In this case, implement the interface to write the event to console (you may modify the method signature as needed and move the file within your project structure to whereever is best).

### Evaluation of The Exercise

We will be looking at your code-style and be gauging your knowledge of software patterns and architecture, clean code, and correctness of the implementation.

As mentioned before, treat this as a production grade application and present your best piece of (code) art possible. Hint: we expect tests across the stack, following of best practice software principles, etc.