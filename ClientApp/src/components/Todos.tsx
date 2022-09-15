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

  useEffect(() => {
    const initialize = async () => {
      setTodos(await (await fetch('todos')).json());
      setLoading(false);
    }
    initialize();
  }, []);

  const renderTodosTable = (todos: ITodo[]) => {
    const [completed, pending] = partition(todos, (v: ITodo) => v.completed);

    return (
      <div>
        <h2>Pending</h2>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {pending.map(todo =>
              <tr key={todo.id}>
                <td>{todo.description}</td>
              </tr>
            )}
          </tbody>
        </table>
        <h2>Completed</h2>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {completed.map(todo =>
              <tr key={todo.id}>
                <td>{todo.description}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  const contents = loading ? <p><em>Loading...</em></p> : renderTodosTable(todos);

  return (
    <div>
      <h1 id="tableLabel">To-dos</h1>
      {contents}
    </div>
  );
}

Todos.displayName = Todos.name;
export default Todos;
