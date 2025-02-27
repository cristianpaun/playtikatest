import axios, { AxiosResponse } from 'axios';

axios.defaults.baseURL = "http://localhost:5000/api";

const responsBody = (response: AxiosResponse) => response.data;

export const request = {
    get: (url: string) => axios.get(url).then(responsBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responsBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responsBody),
    deltete: (url: string, body: {}) => axios.delete(url, body).then(responsBody),
}