import React, {useEffect, useState} from 'react';
import {PowerBiDashboard} from "./PowerBiDashboard";
import {DashboardService} from "../services/DashboardService";

export const Home: React.FC = _ => {
  const [url, setUrl] = useState<string | null>(null);
  const [dashboardAvailable, setDashboardAvailable] = useState(true);
  
  useEffect(() => {
    DashboardService.getDashboardUrlAsync()
      .then(url => {
        setUrl(url)
      })
      .catch(e => {
        console.log("dashboard not available");
        setDashboardAvailable(false);
      });
  }, []);

   return (
      <div>
        <h1>Power BI Dashboard</h1>
        {dashboardAvailable
        ? <PowerBiDashboard url={url} />
        : <p>Power BI Dashboard is not available.</p>}
      </div>
    );
}
