import React, { useState } from "react";

import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { IProduct } from "../../app/models/products";
import { IOrderDetail } from "../../app/models/order-details";

interface IProps {
  products: IProduct[];
  addProduct: (orderDetail: IOrderDetail) => void;
}

export const AddProduct: React.FC<IProps> = ({ products, addProduct }) => {
  const [orderToAdd, setOrderToAdd] = useState<IOrderDetail>({
    productId: -1,
    quantity: 1,
  });

  const handleSubmit = (event) => {
    event.preventDefault();
    addProduct(orderToAdd);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setOrderToAdd({ ...orderToAdd, [name]: value });
  };

  return (
    <div>
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="productList">
          <Form.Label>Choose product:</Form.Label>
          <Form.Control
            required
            as="select"
            onChange={handleInputChange}
            name="productId"
          >
            <option key="-1" value="">
              Choose product...
            </option>
            {products.map((product) => (
              <option key={product.key} value={product.key}>
                {product.name}
              </option>
            ))}
          </Form.Control>
        </Form.Group>
        <Form.Group controlId="qty">
          <Form.Label>Quantity</Form.Label>
          <Form.Control
            required
            min={0}
            type="number"
            onChange={handleInputChange}
            name="quantity"
            defaultValue="1"
          />
        </Form.Group>
        <Button variant="primary" type="submit" style={{ marginTop: "2em" }}>
          Add product
        </Button>
      </Form>
    </div>
  );
};
