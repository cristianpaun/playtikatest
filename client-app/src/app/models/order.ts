import { IOrderDetail } from './order-details';

export interface IOrder 
{
    id?: Number,
    orderDate?: Date,
    cartId: string,
    totalQuantity?: number,
    orderDetails: IOrderDetail[]
}