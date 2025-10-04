import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5272/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;