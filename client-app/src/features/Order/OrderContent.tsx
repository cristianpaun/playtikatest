import React from 'react'
import { IOrder } from '../../app/models/order'
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

interface IProps {
    order: IOrder;
}

export const OrderContent: React.FC<IProps> = ({order}) => {
    return (
        <div>
            <h4>Products in order:</h4>
            {
                order && 
                order.orderDetails.map(orderDetail => (
                    <Row key={orderDetail.productId.toString()}>
                        <Col md="8">{orderDetail.product.name}</Col>
                        <Col md="4">{orderDetail.quantity}</Col>
                    </Row>
                ))
            }        
        </div>
    )
}
