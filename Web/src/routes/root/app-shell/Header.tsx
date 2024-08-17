import { Grid, Burger, UnstyledButton } from "@mantine/core";
import NavBarItems from "./HeaderItems";
import { useNavigate } from "react-router-dom";
import "./HeaderItems.css";
import { NavigateToRoot } from "../../../utilities/NavigateExtensions";

export interface AppShellExProps {
  opened: boolean;
  toggle: () => void;
}

function Header({ opened, toggle }: AppShellExProps) {
  const navigate = useNavigate();

  const OnClickedHomeButton = () => {
    NavigateToRoot(navigate);

    if (opened) {
      toggle();
    }
  };

  return (
    <Grid mt={2} mr={7} grow gutter="0" align="center">
      <Grid.Col span={0}>
        <Burger
          opened={opened}
          onClick={toggle}
          hiddenFrom="sm"
          ml={15}
          color="#c9c9c9"
        />
      </Grid.Col>

      <Grid.Col span={0}>
        <UnstyledButton className="control" onClick={OnClickedHomeButton}>
          Universal Converter
        </UnstyledButton>
      </Grid.Col>

      <Grid.Col visibleFrom="sm" span={1}>
        <NavBarItems toggle={toggle} />
      </Grid.Col>
    </Grid>
  );
}

export default Header;
