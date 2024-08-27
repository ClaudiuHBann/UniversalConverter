import { useNavigate } from "react-router-dom";
import { useConvert } from "../../hooks/Queries";
import { LinkZipRequest } from "../../models/requests/LinkZipRequest";
import { ECategory, ELinkZipFromTo, ESearchParam } from "../../utilities/Enums";
import { LinkZipResponse } from "../../models/responses/LinkZipResponse";
import { useEffect } from "react";
import { Center, Group, Loader, Text, rem } from "@mantine/core";
import { URLSearchParamsEx } from "../../utilities/URLSearchParamsEx";
import { useUCContext } from "../../contexts/UCContext";

const fallbackURL = `/?${ESearchParam.Category}=${ECategory.LinkZip}&${ESearchParam.From}=${ELinkZipFromTo.Longifier}&${ESearchParam.To}=${ELinkZipFromTo.Shortifier}`;

function Redirect() {
  const context = useUCContext();
  const linkZip = useConvert(context!, ECategory.LinkZip);

  const navigate = useNavigate();

  useEffect(() => {
    const searchParams = new URLSearchParamsEx();
    const code = searchParams.GetCode();
    if (!code) {
      navigate(fallbackURL);
      return;
    }

    const request = new LinkZipRequest(
      ELinkZipFromTo.Shortifier,
      ELinkZipFromTo.Longifier,
      [code!]
    );

    linkZip.mutateAsync(request).then((response) => {
      if (!response) {
        navigate(fallbackURL);
        return;
      }

      window.open((response as LinkZipResponse).urls[0], "_self");
    });
  }, []);

  return (
    <Center h="100vh">
      <Group>
        <Text size={rem(18)}>Redirecting...</Text>
        <Loader color="blue" size="md" />
      </Group>
    </Center>
  );
}

export default Redirect;
