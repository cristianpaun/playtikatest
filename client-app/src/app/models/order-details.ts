import { IOrder } from "./order";
import { IProduct } from "./products";

export interface IOrderDetail
{
    id?: Number,
    orderId?: Number,
    productId: Number,
    order?: IOrder,
    product?: IProduct,
    quantity: Number
}