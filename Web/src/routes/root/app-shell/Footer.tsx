import { SimpleGrid, Flex, Anchor, Text, rem } from "@mantine/core";
import {
  IconBrandGoogle,
  IconBrandDiscord,
  IconBrandLinkedin,
  IconBrandGithub,
  IconCheck,
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

function Footer() {
  return (
    <SimpleGrid cols={3}>
      <Flex mih={50} gap="md" justify="center" align="center">
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

      <Flex mih={50} gap="md" justify="center" align="center">
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

      <Flex mih={50} gap="md" justify="center" align="center">
        <Text>Made with ❤️ in Romania by HBann</Text>
      </Flex>
    </SimpleGrid>
  );
}

export default Footer;
