import { Grid, Flex, Anchor, Text, rem, ActionIcon } from "@mantine/core";
import {
  IconBrandGoogle,
  IconBrandDiscord,
  IconBrandLinkedin,
  IconBrandGithub,
  IconCheck,
  IconNotebook,
} from "@tabler/icons-react";
import ActionIconEx from "../../../components/ActionIconEx";

function FindIconGoogle(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconBrandGoogle style={{ width: rem(69), color: "#d44638" }} />;
  }
}

function FindTooltipGoogle(state: boolean) {
  return state ? "Copied Email" : "Copy Email";
}

function FindIconDiscord(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconBrandDiscord style={{ width: rem(69), color: "#7289da" }} />;
  }
}

function FindTooltipDiscord(state: boolean) {
  return state ? "Copied Discord" : "Copy Discord";
}

function ClipboardWriteText(text: string) {
  if (text.length === 0) {
    return;
  }

  navigator.clipboard.writeText(text);
}

const propertiesGridElement = {
  mih: 50,
  gap: "md",
  justify: "center",
  align: "center",
};

export interface FooterProps {
  openedAside: boolean;
  toggleAside: () => void;
}

function Footer({ openedAside, toggleAside }: FooterProps) {
  return (
    <Grid grow justify="center" align="center" gutter="0">
      <Grid.Col span={3}>
        <Flex {...propertiesGridElement}>
          <Text>Contribute on</Text>
          <Anchor
            href="https://github.com/ClaudiuHBann/UniversalConverter"
            c="white"
            ml={-5}
            mt={7.5}
          >
            <IconBrandGithub />
          </Anchor>
        </Flex>
      </Grid.Col>

      <Grid.Col span={3}>
        <Flex {...propertiesGridElement}>
          <Text>Contact Me:</Text>

          <ActionIconEx
            onClick={() => {
              ClipboardWriteText("claudiu.andrei.hermann@gmail.com");
            }}
            findIcon={FindIconGoogle}
            findTooltip={FindTooltipGoogle}
          />

          <Anchor
            href="https://www.linkedin.com/in/hermann-claudiu-b6243a229"
            c="#0077B5"
            mt={5}
          >
            <IconBrandLinkedin />
          </Anchor>

          <ActionIconEx
            onClick={() => {
              ClipboardWriteText("claudiuhbann");
            }}
            findIcon={FindIconDiscord}
            findTooltip={FindTooltipDiscord}
          />
        </Flex>
      </Grid.Col>

      <Grid.Col span={3}>
        <Flex {...propertiesGridElement}>
          <Text>Made with ❤️ in Romania by HBann</Text>
        </Flex>
      </Grid.Col>

      <Grid.Col span="content">
        <Flex {...propertiesGridElement}>
          <ActionIcon
            c="gray"
            variant="subtle"
            onClick={toggleAside}
            style={{ outline: "none" }}
          >
            <IconNotebook />
          </ActionIcon>
        </Flex>
      </Grid.Col>
    </Grid>
  );
}

export default Footer;
