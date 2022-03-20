import { WorldContext } from 'contexts/WorldContext';
import React, { PropsWithChildren, useContext, useEffect } from 'react';
import { Outlet, useNavigate, useParams } from 'react-router-dom';

const World = (props: PropsWithChildren<{}>) => {
  const params = useParams();
  const navigate = useNavigate();
  const { list, setSelectedWorld } = useContext(WorldContext);
  useEffect(() => {
    if (typeof params.world != 'undefined') {
      let selectedWorld = list.find((x) => x.subDomain == params.world) || null;
      setSelectedWorld(selectedWorld);
      if (selectedWorld == null) {
        navigate('/404');
      }
    } else {
      setSelectedWorld(null);
    }
  }, [params.world]);
  return (
    <div>
      <Outlet />
    </div>
  );
};

export default World;
