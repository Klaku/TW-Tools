export const Get = async (url: string) => {
  let response = await fetch(url);
  let body = await response.json();
  return body;
};
