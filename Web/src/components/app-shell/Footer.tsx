import { SimpleGrid, Flex, Anchor, Text, rem } from "@mantine/core";
import {
  IconBrandGoogle,
  IconBrandDiscord,
  IconBrandFacebook,
  IconBrandGithub,
  IconBrandInstagram,
  IconCheck,
} from "@tabler/icons-react";
import ActionIconEx from "../ActionIconEx";

function FindIconGoogle(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconBrandGoogle style={{ width: rem(69) }} />;
  }
}

function FindTooltipGoogle(state: boolean) {
  return state ? "Copied Email" : "Copy Email";
}

function FindIconDiscord(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconBrandDiscord style={{ width: rem(69) }} />;
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
    <SimpleGrid cols={2}>
      <Flex mih={50} gap="md" justify="center" align="center">
        <Text>Contact Me:</Text>
        <ActionIconEx
          onClick={() => {
            ClipboardWriteText("claudiu.andrei.hermann@gmail.com");
          }}
          findIcon={FindIconGoogle}
          findTooltip={FindTooltipGoogle}
        />
        <ActionIconEx
          onClick={() => {
            ClipboardWriteText("claudiuhbann");
          }}
          findIcon={FindIconDiscord}
          findTooltip={FindTooltipDiscord}
        />
      </Flex>
      <Flex mih={50} gap="md" justify="center" align="center">
        <Text>My Socials:</Text>
        <Anchor
          href="https://www.facebook.com/profile.php?id=100011084952722"
          c="gray"
        >
          <IconBrandFacebook />
        </Anchor>
        <Anchor href="https://github.com/ClaudiuHBann" c="gray">
          <IconBrandGithub />
        </Anchor>
        <Anchor href="https://www.instagram.com/claudiuhbann/?hl=en" c="gray">
          <IconBrandInstagram />
        </Anchor>
      </Flex>
    </SimpleGrid>
  );
}

export default Footer;
