import { AppShell } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import Header from "./Header";
import Footer from "./Footer";
import HeaderItems from "./HeaderItems";
import Aside from "./Aside";

export interface AppShellExProps {
  children: React.ReactNode;
}

function AppShellEx({ children }: AppShellExProps) {
  const [openedNavbar, { toggle: toggleNavbar }] = useDisclosure();
  const [openedAside, { toggle: toggleAside }] = useDisclosure();

  return (
    <AppShell
      header={{ height: 50 }}
      navbar={{
        width: 300,
        breakpoint: 0,
        collapsed: { desktop: true, mobile: !openedNavbar },
      }}
      aside={{
        width: "400px",
        breakpoint: 0,
        collapsed: { desktop: !openedAside, mobile: !openedAside },
      }}
      footer={{ height: 50 }}
      padding="md"
    >
      <AppShell.Header>
        <Header openedNavbar={openedNavbar} toggleNavbar={toggleNavbar} />
      </AppShell.Header>

      <AppShell.Navbar>
        <HeaderItems
          openedNavbar={openedNavbar}
          toggleNavbar={toggleNavbar}
          props={{ m: 10, mr: 13, ml: 10 }}
        />
      </AppShell.Navbar>

      <AppShell.Aside>
        <Aside />
      </AppShell.Aside>

      <AppShell.Footer>
        <Footer openedAside={openedAside} toggleAside={toggleAside} />
      </AppShell.Footer>

      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
}

export default AppShellEx;
