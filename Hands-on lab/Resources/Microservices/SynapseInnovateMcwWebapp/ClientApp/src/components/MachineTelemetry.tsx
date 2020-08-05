import React, {useEffect, useState} from "react";
import {QueryService} from "../services/QueryService";
import {DynamicTable} from "./DynamicTable";
import {IMachineTelemetryDto} from "../interfaces/IMachineTelemetryDto";

export const MachineTelemetry: React.FC = _ => {
	const [machineTelemetry, setMachineTelemetry] = useState<IMachineTelemetryDto[] | null>(null);
	
	useEffect(() => {
		QueryService.getMachineTelemetryAsync()
			.then(setMachineTelemetry);
	}, []);

	return (
		<div>
			<h1>MachineTelemetry</h1>
			
			<div>
				{machineTelemetry === null
					? "Loading..."
					: <DynamicTable data={machineTelemetry} hiddenList={["eventId", "entityId", "eventType", "entityType"]} />} 
			</div>
		</div>
	)
}