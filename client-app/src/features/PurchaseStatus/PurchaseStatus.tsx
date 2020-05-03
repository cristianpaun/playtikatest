import React from "react";
import { RouteComponentProps } from "react-router-dom";

export const PurchaseStatus: React.FC<RouteComponentProps> = (route) => {
  let order = route.location.state["detail"];
  let error = route.location.state["error"];

  return (
    <div>
      {order && order !== "" && (
        <div style={{ color: "green", fontSize: "16pt" }}>
          Purchase sent with success!
          <div style={{marginTop: "2em", fontSize:"14pt"}}>
            Please check order status using this number
            <div style={{ marginTop: "1em", color: "blue"}}>{order}</div>
          </div>
        </div>
      )}
      {error && error !== "" && (
        <div style={{ color: "red", fontSize:"16pt" }}>
          Purchase sent with failure!
          <div style={{ marginTop: "2em", color: "black", fontSize:"14pt"}}>
            Error detils:
            <div style={{ color: "red", marginTop: "1em" }}>{error}</div>
          </div>
        </div>
      )}
    </div>
  );
};
