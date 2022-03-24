import React, { PropsWithChildren, useEffect, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { Circle, ImageOverlay, MapContainer, Marker, Rectangle, SVGOverlay, TileLayer } from 'react-leaflet';
const Map = (props: PropsWithChildren<{}>) => {
  const [villages, setVillages] = useState([]);

  useEffect(()=>{
    fetch('/api/Village/GetVillagesByTribeId?worldId=1&tribeId=')
  },[])
  return (
    <Panel.Wrapper>
      <Panel.Title>Interaktywna Mapa świata</Panel.Title>
      <Panel.Section>
        <MapContainer
          style={{ height: 600 }}
          center={[50.5, 51]}
          maxZoom={10}
          maxBounds={[
            [50, 50],
            [51, 52],
          ]}
          bounds={[
            [50, 50],
            [51, 52],
          ]}
          zoom={10}>
          <TileLayer attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors' url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
          <Rectangle
            bounds={[
              [50, 50],
              [51, 52],
            ]}
            pathOptions={{ color: 'red' }}
          />
        </MapContainer>
      </Panel.Section>
    </Panel.Wrapper>
  );
};

export default Map;
