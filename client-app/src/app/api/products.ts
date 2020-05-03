import { request } from './agent'

export const Products = {
    list: () => request.get('/products'),
}