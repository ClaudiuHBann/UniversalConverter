import {
  AppShell,
  Burger,
  Group,
  UnstyledButton,
  Text,
  SimpleGrid,
  Flex,
  Anchor,
  Select,
  Grid,
  GridCol,
  Center,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import {
  IconBrandGithub,
  IconBrandFacebook,
  IconBrandGoogle,
  IconBrandInstagram,
  IconBrandDiscord,
} from "@tabler/icons-react";
import "./NavBar.css";

export interface NavBarProps {
  children: React.ReactNode;
}

function CreateNavBarItems() {
  return (
    <Group justify="space-between">
      <Group gap={0}>
        <UnstyledButton className="control">Currency</UnstyledButton>
        <UnstyledButton className="control">Temperature</UnstyledButton>
        <UnstyledButton className="control">Number.Base</UnstyledButton>
      </Group>

      <Select
        placeholder="Search for a category..."
        data={["Currency", "Temperature", "Number.Base"]}
        maxDropdownHeight={200}
        searchable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

function NavBar({ children }: NavBarProps) {
  const [opened, { toggle }] = useDisclosure();

  return (
    <AppShell
      header={{ height: 50 }}
      navbar={{
        width: 300,
        breakpoint: "sm",
        collapsed: { desktop: true, mobile: !opened },
      }}
      padding="md"
    >
      <AppShell.Header>
        <Grid h="100%" m={0} mt={10} pl={10} pr={15} grow>
          <GridCol span={0}>
            <Burger
              opened={opened}
              onClick={toggle}
              hiddenFrom="sm"
              size="sm"
            />
          </GridCol>

          <GridCol span={0}>
            <Center h="100%" w="100%">
              <Text fw={500} mr={5}>
                Universal Converter
              </Text>
            </Center>
          </GridCol>

          <GridCol span="content" visibleFrom="sm" p={0}>
            {CreateNavBarItems()}
          </GridCol>
        </Grid>
      </AppShell.Header>

      <AppShell.Navbar>{CreateNavBarItems()}</AppShell.Navbar>

      <AppShell.Footer>
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
            <Anchor
              href="https://www.instagram.com/claudiuhbann/?hl=en"
              c="gray"
            >
              <IconBrandGithub />
            </Anchor>
            <Anchor href="https://github.com/ClaudiuHBann" c="gray">
              <IconBrandInstagram />
            </Anchor>
          </Flex>
        </SimpleGrid>
      </AppShell.Footer>

      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
}

export default NavBar;
