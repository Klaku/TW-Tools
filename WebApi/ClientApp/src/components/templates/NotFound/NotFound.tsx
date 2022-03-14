import React, { PropsWithChildren } from "react";

const NotFound = (props:PropsWithChildren<{}>)=>{
    return <div>
        <h1>Oops!</h1>
        <h4>Dzielnie szukałem ale się nie udało.</h4>
        <div>Skorzystaj z nawigacji i obyśmy się tutaj ponownie nie spotkali.</div>
        <div>Error Code: 404</div>
    </div>
}

export default NotFound;