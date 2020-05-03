import React, { useState } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Image from "react-bootstrap/Image";
import { IOrder } from "../../app/models/order";
import { Orders } from "../../app/api/order";
import { OrderContent } from "../Order/OrderContent";
import sad from "../../assets/sad.jpg";
import happy from "../../assets/happy.jpg";

export const OrderDetails = () => {
  const [order, setOrder] = useState<IOrder>(null);
  const [cartId, setCart] = useState("");
  const [displayError, setDisplayError] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [orderConfirmed, setOrderConfirmed] = useState(true);
  let interval = null;

  const searchOrder = (event) => {
    event.preventDefault();
    setDisplayError(false);
    if (cartId.trim() !== "")
      Orders.searchByCartId(cartId.trim())
        .then((response) => {
          let currentTime = new Date();
          currentTime.setSeconds(
            currentTime.getSeconds() - response.totalQuantity
          );
          let orderDate = new Date(response.orderDate);
          let diff = currentTime.getTime() - orderDate.getTime();
          if (diff < 0) {
            setOrderConfirmed(false);
            interval = setInterval(() => {
              setOrderConfirmed(true);
              clearInterval(interval);
            }, -diff);
          } else {
            setOrderConfirmed(true);
          }

          setOrder(response);
        })
        .catch((ex) => {
          setDisplayError(true);
          setOrder(null);
          setErrorMessage(ex.response.data.errors);
        });
  };

  const handleInputChange = (event) => {
    setCart(event.target.value);
  };

  return (
    <div>
      <Form onSubmit={searchOrder}>
        <Form.Group controlId="orderId">
          <Form.Label>Order number:</Form.Label>
          <Form.Control
            placeholder="c0186059-b23e-4304-832d-768225b4befc"
            onChange={handleInputChange}
          />
        </Form.Group>
        <Button variant="primary" style={{ marginTop: "2em" }} type="submit">
          Search Order
        </Button>
      </Form>
      <div
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <div>
          {order && !orderConfirmed && (
            <Image
              style={{ marginTop: "3em", width: "150px" }}
              src={sad}
            ></Image>
          )}
          {order && orderConfirmed && (
            <Image
              style={{ marginTop: "3em", width: "150px" }}
              src={happy}
            ></Image>
          )}
        </div>
        <div>
          {order && orderConfirmed && (
            <h3 style={{ color: "green" }}>ORDER CONFIRMED!</h3>
          )}
          {order && !orderConfirmed && (
            <h3 style={{ color: "red" }}>ORDER UNCONFIRMED!</h3>
          )}
          {order && <h3>Order Id: {order.cartId}</h3>}
        </div>
      </div>
      <div style={{ marginTop: "3em" }}>
        {order && <OrderContent order={order}></OrderContent>}
        {displayError && (
          <div style={{ marginTop: "2em", color: "red", fontSize: "14pt" }}>
            {errorMessage}
          </div>
        )}
      </div>
    </div>
  );
};
