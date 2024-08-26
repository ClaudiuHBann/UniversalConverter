import { Grid, Burger, UnstyledButton } from "@mantine/core";
import HeaderItems from "./HeaderItems";
import { useNavigate } from "react-router-dom";
import "./HeaderItems.css";
import { NavigateToRoot } from "../../../utilities/NavigateExtensions";

export interface HeaderProps {
  openedNavbar: boolean;
  toggleNavbar: () => void;
}

function Header({ openedNavbar, toggleNavbar }: HeaderProps) {
  const navigate = useNavigate();

  const OnClickedHomeButton = () => {
    NavigateToRoot(navigate);

    if (openedNavbar) {
      toggleNavbar();
    }
  };

  return (
    <Grid mt={2} mr={7} grow gutter="0" align="center">
      <Grid.Col span={0}>
        <Burger
          opened={openedNavbar}
          onClick={toggleNavbar}
          hiddenFrom="sm"
          ml={15}
          mr={15}
          pl={5}
          color="#c9c9c9"
        />
      </Grid.Col>

      <Grid.Col span={0}>
        <UnstyledButton className="control" onClick={OnClickedHomeButton}>
          Universal Converter
        </UnstyledButton>
      </Grid.Col>

      <Grid.Col visibleFrom="sm" span={1}>
        <HeaderItems openedNavbar={openedNavbar} toggleNavbar={toggleNavbar} />
      </Grid.Col>
    </Grid>
  );
}

export default Header;
