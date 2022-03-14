import { ComboBox } from "@fluentui/react";
import React, { PropsWithChildren, useState } from "react";
import * as Styled from "./Plan.styles";
const Plan = (props:PropsWithChildren<{}>)=>{
    let [tribes, setTribes] = useState([])
    return <Styled.Plan_Container>
        <h3>Czas rozplanować kolejną akcję?</h3>
        <hr></hr>
        <h4>Zacznijmy od wyboru stron konfliktu.</h4>
        <Styled.Combo_Container>
            <ComboBox label="Strona Atakująca" options={tribes}></ComboBox>
            <ComboBox label="Wrogowie" options={tribes}></ComboBox>
        </Styled.Combo_Container>
    </Styled.Plan_Container>
}

export default Plan;