import styled from 'styled-components';
import { TribeContext } from 'contexts/TribeContext';
import { VillageContext } from 'contexts/VillageContext';
import { WorldContext } from 'contexts/WorldContext';
import L, { LatLngTuple } from 'leaflet';
import React, { useContext } from 'react';
import { LayerGroup, MapContainer, Polygon, Tooltip } from 'react-leaflet';
import { Village } from 'types/Village';
import { IGroup } from '../MapOperations/MapOperations';
import { PlayerContext } from 'contexts/PlayerContext';

export interface MapProps {
  villages: Village[];
  groups: IGroup[];
}

export const Map = (props: MapProps) => {
  const context = {
    Tribe: useContext(TribeContext),
    Player: useContext(PlayerContext),
  };
  return (
    <MapContainer
      style={{ height: 600, width: 600 }}
      crs={L.CRS.Simple}
      center={[-500, 500]}
      maxBounds={[
        [0, 0],
        [-1000, 1000],
      ]}
      zoom={1}>
      {props.groups.map((group) => {
        return props.villages
          .filter((x) => group.tribes.map((a) => a.TribeId).indexOf(x.TribeId) != -1)
          .map((x) => {
            return (
              <Polygon
                stroke={false}
                key={`${x.Id}_${group.key}`}
                fillOpacity={0.7}
                fillColor={group.color.str}
                positions={[
                  [-1 * x.PositionY, x.PositionX],
                  [-1 * x.PositionY + 1, x.PositionX],
                  [-1 * x.PositionY + 1, x.PositionX + 1],
                  [-1 * x.PositionY, x.PositionX + 1],
                ]}>
                <Tooltip>
                  <TooltipWraper>
                    <div>{context.Tribe.tribes.filter((a) => a.TribeId == x.TribeId)[0].Name}</div>
                    <div>
                      {x.PositionX}|{x.PositionY}
                    </div>
                    <div>{context.Player.players.filter((a) => a.PlayerId == x.PlayerId)[0]?.Name}</div>
                    <div>{x.Points} punktów</div>
                  </TooltipWraper>
                </Tooltip>
              </Polygon>
            );
          });
      })}
    </MapContainer>
  );
};

const TooltipWraper = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
`;
