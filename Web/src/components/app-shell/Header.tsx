import { Grid, GridCol, Burger, UnstyledButton } from "@mantine/core";
import NavBarItems from "./HeaderItems";
import { useNavigate } from "react-router-dom";
import "./HeaderItems.css";

export interface AppShellExProps {
  opened: boolean;
  toggle: () => void;
}

function Header({ opened, toggle }: AppShellExProps) {
  const navigate = useNavigate();

  return (
    <Grid h="100%" m={0} mt={10} pl={10} pr={15} grow>
      <GridCol span={0}>
        <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
      </GridCol>

      <GridCol span={0} p={0} pr={10}>
        <UnstyledButton className="control" onClick={() => navigate("/")}>
          Universal Converter
        </UnstyledButton>
      </GridCol>

      <GridCol span="content" visibleFrom="sm" p={0}>
        <NavBarItems />
      </GridCol>
    </Grid>
  );
}

export default Header;
