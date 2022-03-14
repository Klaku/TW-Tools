import { ComboBox } from "@fluentui/react";
import { WorldContext } from "contexts/WorldContext";
import React, { PropsWithChildren, useContext } from "react";
import * as Styled from "./NavigationFooter.styles";
const NavigationFooter = (props: PropsWithChildren<{}>) => {
  const { list, fetching, setSelectedWorld, selected } =
    useContext(WorldContext);
  return (
    <Styled.Wrapper>
      {!fetching && (
        <ComboBox
          useComboBoxAsMenuWidth={true}
          options={list.map((world) => {
            return {
              key: world.id,
              text: world.name,
            };
          })}
          onItemClick={(event, option) => {
            setSelectedWorld(
              option ? list.filter((x) => x.id == option.key)[0] : null
            );
          }}
          selectedKey={selected ? selected.id : null}
        />
      )}
    </Styled.Wrapper>
  );
};
export default NavigationFooter;
