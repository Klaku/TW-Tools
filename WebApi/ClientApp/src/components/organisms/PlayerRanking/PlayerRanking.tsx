import React, { PropsWithChildren, useContext, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { TribeContext } from 'contexts/TribeContext';
import { Item, ListWrapper, Col, HeaderRow, DataContainer, DataRow, DataItem, ExpandContainer } from './PlayerRanking.styles';
import ValueColor from 'components/atoms/ValueColor';
import { Icon } from '@fluentui/react';
import { Tribe } from 'types/Tribe';
import { useNavigate, useParams } from 'react-router-dom';
import { WorldContext } from 'contexts/WorldContext';
import { PlayerContext } from 'contexts/PlayerContext';
import { Player } from 'types/Player';

const PlayerRanking = (props: PropsWithChildren<{ filter: string }>) => {
  const Contexts = {
    Player: useContext(PlayerContext),
    World: useContext(WorldContext),
  };
  const { sort, method } = useParams();
  const navigate = useNavigate();
  const redirect = (key: string) => {
    if (sort == key && method != 'desc') {
      navigate(`/${Contexts.World.selected?.subDomain}/rank/player/${key}/desc`);
    } else {
      navigate(`/${Contexts.World.selected?.subDomain}/rank/player/${key}/asc`);
    }
  };
  const keySort = (a: Player, b: Player) => {
    let c, d;
    switch (sort) {
      case 'nr':
      case 'points':
        c = a.points;
        d = b.points;
        break;
      case 'name':
        c = a.name;
        d = b.name;
        break;
      case 'villages':
        c = a.villagesCount;
        d = b.villagesCount;
        break;
      case 'ra':
        c = a.ra;
        d = b.ra;
        break;
      case 'ro':
        c = a.ro;
        d = b.ro;
        break;
      case 'rs':
        c = a.rs;
        d = b.rs;
        break;
      default:
        d = a.ranking;
        c = b.ranking;
        break;
    }
    return (c < d ? -1 : 1) * (method == 'desc' ? 1 : -1);
  };
  const VisiblePlayers = Contexts.Player.players.filter((x) => x.name.toLowerCase().indexOf(props.filter.toLowerCase()) != -1);
  return (
    <Panel.Section>
      <Panel.Title2>Ranking graczy</Panel.Title2>
      <ListWrapper>
        <HeaderRow>
          <Col
            onClick={() => {
              redirect('nr');
            }}
            style={{ width: 25, cursor: 'pointer' }}>
            #
          </Col>
          <Col style={{ width: 50 }}>Plemie</Col>
          <Col
            onClick={() => {
              redirect('name');
            }}
            style={{ minWidth: 50, flexGrow: 1, cursor: 'pointer' }}>
            Nazwa
          </Col>
          <Col
            onClick={() => {
              redirect('points');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            Punkty
          </Col>
          <Col
            onClick={() => {
              redirect('villages');
            }}
            style={{ width: 60, cursor: 'pointer' }}>
            Wioski
          </Col>
          <Col
            onClick={() => {
              redirect('ra');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RA
          </Col>
          <Col
            onClick={() => {
              redirect('ro');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RO
          </Col>
          <Col
            onClick={() => {
              redirect('rs');
            }}
            style={{ width: 100, cursor: 'pointer' }}>
            RS
          </Col>
          <ExpandContainer></ExpandContainer>
        </HeaderRow>
        {VisiblePlayers.sort(keySort).map((player) => {
          return <ListItemComponent key={player.id} player={player} />;
        })}
      </ListWrapper>
    </Panel.Section>
  );
};

const ListItemComponent = (props: PropsWithChildren<{ player: Player }>) => {
  const { player } = props;
  const [isCollapsed, setIsCollapsed] = useState(true);
  const Tribe = useContext(TribeContext);
  const RowData = (title: string, val: number, val24: number, val7: number, val30: number) => {
    return (
      <DataRow>
        <DataItem style={{ minWidth: 150 }}>{title}</DataItem>
        <DataItem>{Number(val).toLocaleString('de')}</DataItem>
        <DataItem>
          {val24 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val24)}>
              {Math.abs(Number(val - val24)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <DataItem>
          {val7 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val7)}>
              {Math.abs(Number(val - val7)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <DataItem>
          {val30 == -1 ? (
            'Brak danych'
          ) : (
            <ValueColor forceSign={true} value={Number(val - val30)}>
              {Math.abs(Number(val - val30)).toLocaleString('de')}
            </ValueColor>
          )}
        </DataItem>
        <ExpandContainer></ExpandContainer>
      </DataRow>
    );
  };
  return (
    <Item>
      <HeaderRow>
        <Col style={{ width: 25 }}>{player.ranking}</Col>
        <Col style={{ minWidth: 50 }}>{Tribe.tribes.filter((x) => x.tribeId == player.tribeId)[0]?.tag}</Col>
        <Col style={{ minWidth: 50, flexGrow: 1 }}>{player.name}</Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.points - player.points24}>{Number(player.points).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 60 }}>
          <ValueColor value={player.villagesCount - player.villagesCount24}>{Number(player.villagesCount).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.ra - player.rA24}>{Number(player.ra).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.ro - player.rO24}>{Number(player.ro).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={player.rs - player.rS24}>{Number(player.rs).toLocaleString('de')}</ValueColor>
        </Col>
        {isCollapsed ? (
          <ExpandContainer
            style={{ cursor: 'pointer' }}
            onClick={() => {
              setIsCollapsed(false);
            }}>
            <Icon iconName={'ChevronDown'} />
          </ExpandContainer>
        ) : (
          <ExpandContainer
            style={{ cursor: 'pointer' }}
            onClick={() => {
              setIsCollapsed(true);
            }}>
            <Icon iconName={'ChevronUp'} />
          </ExpandContainer>
        )}
      </HeaderRow>
      {!isCollapsed && (
        <DataContainer>
          <DataRow style={{ backgroundColor: '#ddd' }}>
            <DataItem style={{ minWidth: 150 }}>Właściwość</DataItem>
            <DataItem>Aktualnie</DataItem>
            <DataItem>24 godziny</DataItem>
            <DataItem>7 dni</DataItem>
            <DataItem>30dni</DataItem>
            <ExpandContainer></ExpandContainer>
          </DataRow>
          {RowData('Ranking', player.ranking, player.ranking24, player.ranking7, player.ranking30)}
          {RowData('Liczba Punktów', player.points, player.points24, player.points7, player.points30)}
          {RowData('Liczba Wiosek', player.villagesCount, player.villagesCount24, player.villagesCount7, player.villagesCount30)}
          {RowData('Pokonani w Ataku', player.ra, player.rA24, player.rA7, player.rA30)}
          {RowData('Pokonani w Obronie', player.ro, player.rO24, player.rO7, player.rO30)}
          {RowData('Pokonani Razem', player.rs, player.rS24, player.rS7, player.rS30)}
        </DataContainer>
      )}
    </Item>
  );
};

export default PlayerRanking;
