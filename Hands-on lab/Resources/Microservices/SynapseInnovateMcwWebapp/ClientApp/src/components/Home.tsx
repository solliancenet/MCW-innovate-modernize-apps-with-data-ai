import React, {useEffect, useState} from 'react';
import {PowerBiDashboard} from "./PowerBiDashboard";
import {DashboardService} from "../services/DashboardService";

export const Home: React.FC = _ => {
  const [url, setUrl] = useState<string | null>(null);
  
  useEffect(() => {
    DashboardService.getDashboardUrlAsync()
      .then(url => {
        setUrl(url)
      });
  }, []);

   return (
      <div>
        <h1>Power BI Dashboard</h1>
        <PowerBiDashboard url={url} />
      </div>
    );
}
