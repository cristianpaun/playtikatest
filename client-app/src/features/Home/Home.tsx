import React, { useState, useEffect } from "react";
import { Orders } from "../../app/api/order";
import { Products } from "../../app/api/products";
import { IProduct } from "../../app/models/products";
import { AddProduct } from "../Product/AddProduct";
import { IOrder } from "../../app/models/order";
import { IOrderDetail } from "../../app/models/order-details";
import { OrderContent } from "../Order/OrderContent";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import { v4 as uuid } from 'uuid';
import { RouteComponentProps } from "react-router-dom";

export const Home : React.FC<RouteComponentProps> = (route) => {
  const [products, setProducts] = useState<IProduct[]>([]);
  const [order, setOrder] = useState<IOrder>({
    id: 0,
    cartId: uuid(),
    orderDetails: [],
  });

  useEffect(() => {
    Products.list().then((response) => setProducts(response));
  }, []);

  const handleAddProduct = (orderDetail: IOrderDetail) => {
    orderDetail.product = products.find(
      (p) => p.key === +orderDetail.productId
    );
    orderDetail.productId = +orderDetail.productId;
    orderDetail.quantity = +orderDetail.quantity;
    if (
      order &&
      order.orderDetails.some((o) => +o.productId === +orderDetail.productId)
    ) {
      alert(
        `${orderDetail.product.name} was already added! if you no longer have to add products you can submit the purchase!`
      );
      return;
    }
    setOrder({ ...order, orderDetails: [...order?.orderDetails, orderDetail] });
  };

  const placeOrder = () => {
    let orderToSend = {
      cartId: order.cartId,
      products: order.orderDetails.map((detail) => {
        return { productId: detail.productId, quantity: detail.quantity };
      })
    }
      
    Orders.placeOrder(orderToSend)
      .then(response => {
        route.history.push('/purchase', { detail: order.cartId, error: "" });
      })
      .catch(err => {
        route.history.push('/purchase', { detail:"", error: `${err.response.data.errors}` });
      });  
    
    setOrder({
      id: 0,
      cartId: uuid(),
      orderDetails: [],
    });
  };

  return (
    <div>
      <Row>
        <Col md="8">
          <AddProduct
            products={products}
            addProduct={handleAddProduct}
          ></AddProduct>
        </Col>
        <Col md="1"></Col>
        <Col md="3">
          <OrderContent order={order}></OrderContent>
          {order && order.orderDetails.length > 0 && (
            <Button
              variant="primary"
              style={{ marginTop: "2em" }}
              onClick={placeOrder}
            >
              Submit the purchase
            </Button>
          )}
        </Col>
      </Row>
    </div>
  );
};
