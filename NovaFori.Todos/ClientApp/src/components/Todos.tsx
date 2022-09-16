import React, { useEffect, useState } from 'react';
import { partition } from 'lodash';

interface ITodo {
  id: number,
  description: string,
  completed: boolean,
}

const Todos: React.FC = () => {
  const [todos, setTodos] = useState<ITodo[]>([]);
  const [loading, setLoading] = useState(true);
  const [newTodo, setNewTodo] = useState<string>('');

  const loadTodos = async (): Promise<void> => {
    setLoading(true);
    setTodos(await (await fetch('todos')).json());
    setLoading(false);
  }

  useEffect(() => {
    const initialize = async () => {
      await loadTodos();
    }
    initialize();
  }, []);

  const updateNewTodo = (e: any) => {
    setNewTodo(e.target.value);
  }

  const toggleTodo = async (todoId: number, completed: boolean) => {
    await fetch(`todos/${todoId}?completed=${completed}`, { method: 'PATCH' });
    await loadTodos();
  }

  const addTodo = async (): Promise<void> => {
    const todo = await (await fetch(`todos?description=${newTodo}`, { method: 'POST' })).json();

    todos.push(todo);
    setTodos([...todos]);
  }

  const renderTodosTable = (todos: ITodo[]) => {
    const [completed, pending] = partition(todos, (v: ITodo) => v.completed);

    return (
      <div>
        <h2 id="pendingHeader">Pending</h2>
        <table className='table table-striped' aria-labelledby="pendingHeader">
          <thead>
            <tr>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {pending.map(todo =>
              <tr key={todo.id}>
                <td>{todo.description}</td>
                <td><button type="button" onClick={() => toggleTodo(todo.id, true)}>✅</button></td>
              </tr>
            )}
          </tbody>
        </table>
        <h2 id="completedHeader">Completed</h2>
        <table className='table table-striped' aria-labelledby="completedHeader">
          <thead>
            <tr>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {completed.map(todo =>
              <tr key={todo.id}>
                <td>{todo.description}</td>
                <td><button type="button" onClick={() => toggleTodo(todo.id, false)}>❌</button></td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  const contents = loading ? <p><em>Loading...</em></p> : renderTodosTable(todos);
  const style = { padding: 5 };

  return (
    <div>
      <div>
        <label style={style} htmlFor="endDate">New To-do:</label>
        <input type="text" id="endDate" value={newTodo} onChange={updateNewTodo} />
        <button type="button" disabled={newTodo.length === 0} onClick={addTodo}>Add</button>
      </div>
      <h1>To-dos</h1>
      {contents}
    </div>
  );
}

Todos.displayName = Todos.name;
export default Todos;
