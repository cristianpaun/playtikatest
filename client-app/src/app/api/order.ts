import { request } from './agent';

export const Orders = {
    placeOrder: (order) => request.post('/orders', order),
    searchByCartId: (cartId) => request.get(`/orders/${cartId}`),
}