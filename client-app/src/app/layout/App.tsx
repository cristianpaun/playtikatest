import React from "react";
import Container from "react-bootstrap/Container";
import { Route } from "react-router-dom";
import { MainMenu } from "../../features/nav/MainMenu";
import { Home } from "../../features/Home/Home";
import { PurchaseStatus } from "../../features/PurchaseStatus/PurchaseStatus";
import { OrderDetails } from "../../features/OrderDetail/OrderDetails";

const App = () => {
  return (
    <div>
      <MainMenu />
      <Container style={{ marginTop: "7em" }}>
        <Route path="/purchase" component={ PurchaseStatus }></Route>
        <Route path="/orders" component={ OrderDetails }></Route>
        <Route exact path="/" component={Home}></Route>
      </Container>
    </div>
  );
};

export default App;
