import axios from 'axios'

let baseURL = location.port != "5001" ? "https://localhost:5001" : ""

export let api = axios.create({
  baseURL: baseURL + '/api',
  timeout: 8000,
  withCredentials: true
})

export let auth = axios.create({
  baseURL: baseURL + '/account',
  timeout: 8000,
  withCredentials: true
})

export let user = {}