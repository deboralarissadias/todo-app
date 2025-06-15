<template>
  <div class="todo">
    <h1>Lista de Tarefas</h1>
    <form @submit.prevent="addTask">
      <input v-model="newTask" placeholder="Nova tarefa..." />
      <button type="submit">Adicionar</button>
    </form>
    <ul>
      <li v-for="item in tasks" :key="item.id">
        <input type="checkbox" v-model="item.isDone" @change="updateTask(item)" />
        <span :style="{ textDecoration: item.isDone ? 'line-through' : 'none' }">
          {{ item.title }}
        </span>
        <button @click="deleteTask(item.id)" title="Remover">🗑️</button>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'TodoList',
  data() {
    return {
      newTask: '',
      tasks: [],
    };
  },
  methods: {
    async fetchTasks() {
      const res = await axios.get('http://localhost:5020/api/todo');
      this.tasks = res.data;
    },
    async addTask() {
      if (!this.newTask.trim()) return;
      await axios.post('http://localhost:5020/api/todo', {
        title: this.newTask,
        isDone: false,
      });
      this.newTask = '';
      this.fetchTasks();
    },
    async updateTask(item) {
      await axios.put(`http://localhost:5020/api/todo/${item.id}`, item);
    },
    async deleteTask(id) {
      await axios.delete(`http://localhost:5020/api/todo/${id}`);
      this.fetchTasks();
    },
  },
  mounted() {
    this.fetchTasks();
  },
};
</script>

<style>

html {
  background-color: rgb(240, 240, 240);
}
.todo {
  max-width: 500px;
  margin: 40px auto;
  padding: 30px;
  background-color: rgb(240, 240, 240);
  border-radius: 12px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  font-family: sans-serif;
}

.todo h1 {
  text-align: center;
  color: #333;
  margin-bottom: 20px;
}

.todo form {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}

.todo input[type="text"] {
  flex: 1;
  padding: 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.todo button {
  padding: 10px 16px;
  border: none;
  background-color: rgb(180, 180, 180);
  color: white;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.todo button:hover {
  background-color: rgb(180, 180, 180);
}

.todo ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.todo li {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #fff;
  padding: 10px 12px;
  margin-bottom: 10px;
  border-radius: 6px;
  transition: box-shadow 0.2s ease;
}

.todo li:hover {
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
}

.todo li span {
  flex: 1;
  margin-left: 10px;
}

.todo li button {
  background-color: transparent;
  border: none;
  color: #f87171;
  font-weight: bold;
  cursor: pointer;
}

</style>
