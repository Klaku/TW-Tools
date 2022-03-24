import React from 'react';
import ReactDOM from 'react-dom';
import Provider from 'providers/Provider';
import 'assets/styles.css';
import 'leaflet/dist/leaflet.css';
import Main from 'views/Main/Main';
ReactDOM.render(
  <React.StrictMode>
    <Provider>
      <Main />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);
