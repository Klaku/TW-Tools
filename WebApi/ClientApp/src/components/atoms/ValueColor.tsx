import React, { PropsWithChildren } from 'react';

const ValueColor = (props: PropsWithChildren<{ value: number }>) => {
  return <div style={props.value == 0 ? {} : props.value < 0 ? { color: '#d00' } : { color: '#0d0' }}>{`${props.value < 0 ? '-' : '+'}${props.children}`}</div>;
};

export default ValueColor;
