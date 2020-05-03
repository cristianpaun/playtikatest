import React from "react";

import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import Container from "react-bootstrap/Container";
import { NavLink } from "react-router-dom";

export const MainMenu = () => {
  return (
    <div>
      <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
        <Container>
          <Navbar.Brand href="/">KlayTipa Gardens</Navbar.Brand>
          <Container>
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="mr-auto">
                <NavLink to="/orders">Orders</NavLink>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Container>
      </Navbar>
    </div>
  );
};
