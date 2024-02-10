import { SimpleGrid, Flex, Anchor, Text } from "@mantine/core";
import {
  IconBrandGoogle,
  IconBrandDiscord,
  IconBrandFacebook,
  IconBrandGithub,
  IconBrandInstagram,
} from "@tabler/icons-react";

function Footer() {
  return (
    <SimpleGrid cols={2}>
      <Flex mih={50} gap="md" justify="center" align="center">
        <Text>Contact Me:</Text>
        <Anchor href="http://162.55.32.18:80/" c="gray">
          <IconBrandGoogle />
        </Anchor>
        <Anchor href="http://162.55.32.18:80/" c="gray">
          <IconBrandDiscord />
        </Anchor>
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
