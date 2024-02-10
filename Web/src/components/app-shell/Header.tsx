import { Grid, GridCol, Text, Burger, Center, rem } from "@mantine/core";
import NavBarItems from "./HeaderItems";

export interface AppShellExProps {
  opened: boolean;
  toggle: () => void;
}

function Header({ opened, toggle }: AppShellExProps) {
  return (
    <Grid h="100%" m={0} mt={10} pl={10} pr={15} grow>
      <GridCol span={0}>
        <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
      </GridCol>

      <GridCol span={0}>
        <Center h="100%" w="100%">
          <Text fs={rem(2)} fw={500} mr={5}>
            Universal Converter
          </Text>
        </Center>
      </GridCol>

      <GridCol span="content" visibleFrom="sm" p={0}>
        <NavBarItems />
      </GridCol>
    </Grid>
  );
}

export default Header;
