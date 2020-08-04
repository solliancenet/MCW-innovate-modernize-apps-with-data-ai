import React, {useEffect, useState} from "react";
import {QueryService} from "../services/QueryService";
import {IMetadataDto} from "../interfaces/IMetadataDto";
import {DynamicTable} from "./DynamicTable";
import {AddFactory} from "./AddFactory";
import {AddMachine} from "./AddMachine";
import {AddMaintenanceLookup} from "./AddMaintenanceLookup";

export const Metadata: React.FC = _ => {
	const [metadata, setMetadata] = useState<IMetadataDto | null>(null)
	
	useEffect(() => {
		QueryService.getMetadataAsync()
			.then(setMetadata);
	}, [])
	
	return (
		<div>
			<h1>Metadata</h1>
			
			<div>
				{metadata === null 
					? "Loading..." 
					: <>
						{metadata.metadataFactories && metadata.metadataFactories.length
							? <>
								<h2>Factories</h2>
								<AddFactory addFactoryCallback={mf => setMetadata({...metadata, metadataFactories: [mf, ...metadata?.metadataFactories]})} />
								<DynamicTable data={metadata.metadataFactories} hiddenList={["entityType"]} />
							</>
							: null}

						{metadata.metadataMachines && metadata.metadataMachines.length
							? <>
								<h2>Machines</h2>
								<AddMachine addMachineCallback={m => setMetadata({...metadata, metadataMachines: [m, ...metadata?.metadataMachines]})} />
								<DynamicTable data={metadata.metadataMachines} hiddenList={["entityType"]} />
							</>
							: null}

						{metadata.metadataMaintenanceLookups && metadata.metadataMaintenanceLookups.length
							? <>
								<h2>Maintenance Lookups</h2>
								<AddMaintenanceLookup addMaintenanceLookupCallback={ml => setMetadata({...metadata, metadataMaintenanceLookups: [ml, ...metadata?.metadataMaintenanceLookups]})} />
								<DynamicTable data={metadata.metadataMaintenanceLookups} hiddenList={["entityType"]} />
							</>
							: null}
					</>}
			</div>
		</div>
	)
}