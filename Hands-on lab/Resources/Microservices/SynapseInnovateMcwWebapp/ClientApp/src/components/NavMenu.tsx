import React, {useState} from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export const NavMenu: React.FC<{}> = () => {
  const [collapsed, setCollapsed] = useState(false);
  
  return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">SynapseInnovateMcwWebapp</NavbarBrand>
            <NavbarToggler onClick={() => setCollapsed(!collapsed)} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Power BI App</NavLink>
                </NavItem>

                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/metadata">Metadata</NavLink>
                </NavItem>

                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/machinetelemetry">MachineTelemetry</NavLink>
                </NavItem>
                
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
  );
}