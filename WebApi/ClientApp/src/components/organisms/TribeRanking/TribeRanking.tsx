import React, { PropsWithChildren, useContext, useState } from 'react';
import * as Panel from 'components/templates/Panel/PanelWrapper.styles';
import { TribeContext } from 'contexts/TribeContext';
import { Item, ListWrapper, Col, HeaderRow, DataContainer, DataRow, DataItem, ExpandContainer } from './TribeRanking.styles';
import ValueColor from 'components/atoms/ValueColor';
import { Icon } from '@fluentui/react';
import { Tribe } from 'types/Tribe';
import { useNavigate, useParams } from 'react-router-dom';
import { WorldContext } from 'contexts/WorldContext';

const TribeRanking = (props: PropsWithChildren<{ filter: string }>) => {
  const Contexts = {
    Tribe: useContext(TribeContext),
    World: useContext(WorldContext),
  };
  const { sort, method } = useParams();
  const navigate = useNavigate();
  const redirect = (key: string) => {
    if (sort == key && method != 'desc') {
      navigate(`/${Contexts.World.selected?.subDomain}/rank/tribe/${key}/desc`);
    } else {
      navigate(`/${Contexts.World.selected?.subDomain}/rank/tribe/${key}/asc`);
    }
  };
  const keySort = (a: Tribe, b: Tribe) => {
    let c, d;
    switch (sort) {
      case 'nr':
      case 'points':
        c = a.points;
        d = b.points;
        break;
      case 'tag':
        c = a.tag;
        d = b.tag;
        break;
      case 'name':
        c = a.name;
        d = b.name;
        break;
      case 'villages':
        c = a.villages;
        d = b.villages;
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
  const VisibleTribes = Contexts.Tribe.tribes.filter(
    (x) => x.name.toLocaleLowerCase().indexOf(props.filter.toLowerCase()) != -1 || x.tag.toLocaleLowerCase().indexOf(props.filter.toLowerCase()) != -1
  );
  return (
    <Panel.Section>
      <Panel.Title2>Ranking plemion</Panel.Title2>
      <ListWrapper>
        <HeaderRow>
          <Col
            onClick={() => {
              redirect('nr');
            }}
            style={{ width: 25, cursor: 'pointer' }}>
            #
          </Col>
          <Col
            onClick={() => {
              redirect('tag');
            }}
            style={{ width: 60, cursor: 'pointer' }}>
            Tag
          </Col>
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
        {VisibleTribes.sort(keySort).map((tribe) => {
          return <ListItemComponent key={tribe.id} tribe={tribe} />;
        })}
        {props.filter.length > 1 && (
          <ListItemComponent
            tribe={
              {
                id: 0,
                worldId: 0,
                tribeId: VisibleTribes[0].tribeId,
                name: 'Sumarycznie',
                tag: '',
                ra: VisibleTribes.map((x) => x.ra).reduce((a, b) => a + b),
                villages: VisibleTribes.map((x) => x.villages).reduce((a, b) => a + b),
                villages7: VisibleTribes.map((x) => x.villages7).reduce((a, b) => a + b),
                villages24: VisibleTribes.map((x) => x.villages24).reduce((a, b) => a + b),
                villages30: VisibleTribes.map((x) => x.villages30).reduce((a, b) => a + b),
                points: VisibleTribes.map((x) => x.points).reduce((a, b) => a + b),
                points7: VisibleTribes.map((x) => x.points7).reduce((a, b) => a + b),
                points24: VisibleTribes.map((x) => x.points24).reduce((a, b) => a + b),
                points30: VisibleTribes.map((x) => x.points30).reduce((a, b) => a + b),
                rA7: VisibleTribes.map((x) => x.rA7).reduce((a, b) => a + b),
                rA24: VisibleTribes.map((x) => x.rA24).reduce((a, b) => a + b),
                rA30: VisibleTribes.map((x) => x.rA30).reduce((a, b) => a + b),
                ro: VisibleTribes.map((x) => x.ro).reduce((a, b) => a + b),
                rO7: VisibleTribes.map((x) => x.rO7).reduce((a, b) => a + b),
                rO24: VisibleTribes.map((x) => x.rO24).reduce((a, b) => a + b),
                rO30: VisibleTribes.map((x) => x.rO30).reduce((a, b) => a + b),
                rs: VisibleTribes.map((x) => x.rs).reduce((a, b) => a + b),
                rS7: VisibleTribes.map((x) => x.rS7).reduce((a, b) => a + b),
                rS24: VisibleTribes.map((x) => x.rS24).reduce((a, b) => a + b),
                rS30: VisibleTribes.map((x) => x.rS30).reduce((a, b) => a + b),
                ranking: VisibleTribes.map((x) => x.ranking).reduce((a, b) => a + b),
                ranking7: VisibleTribes.map((x) => x.ranking7).reduce((a, b) => a + b),
                ranking24: VisibleTribes.map((x) => x.ranking24).reduce((a, b) => a + b),
                ranking30: VisibleTribes.map((x) => x.ranking30).reduce((a, b) => a + b),
              } as Tribe
            }
          />
        )}
      </ListWrapper>
    </Panel.Section>
  );
};

const ListItemComponent = (props: PropsWithChildren<{ tribe: Tribe }>) => {
  const { tribe } = props;
  const [isCollapsed, setIsCollapsed] = useState(true);
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
        <Col style={{ width: 25 }}>{tribe.ranking}</Col>
        <Col style={{ width: 60 }}>{tribe.tag}</Col>
        <Col style={{ minWidth: 50, flexGrow: 1 }}>{tribe.name}</Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.points - tribe.points24}>{Number(tribe.points).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 60 }}>
          <ValueColor value={tribe.villages - tribe.villages24}>{Number(tribe.villages).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.ra - tribe.rA24}>{Number(tribe.ra).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.ro - tribe.rO24}>{Number(tribe.ro).toLocaleString('de')}</ValueColor>
        </Col>
        <Col style={{ width: 100 }}>
          <ValueColor value={tribe.rs - tribe.rS24}>{Number(tribe.rs).toLocaleString('de')}</ValueColor>
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
          {tribe.id != 0 && RowData('Ranking', tribe.ranking, tribe.ranking24, tribe.ranking7, tribe.ranking30)}
          {RowData('Liczba Punktów', tribe.points, tribe.points24, tribe.points7, tribe.points30)}
          {RowData('Liczba Wiosek', tribe.villages, tribe.villages24, tribe.villages7, tribe.villages30)}
          {RowData('Pokonani w Ataku', tribe.ra, tribe.rA24, tribe.rA7, tribe.rA30)}
          {RowData('Pokonani w Obronie', tribe.ro, tribe.rO24, tribe.rO7, tribe.rO30)}
          {RowData('Pokonani Razem', tribe.rs, tribe.rS24, tribe.rS7, tribe.rS30)}
        </DataContainer>
      )}
    </Item>
  );
};

export default TribeRanking;
