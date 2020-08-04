import React, {useState} from "react";
import {IMetadataMaintenanceLookup} from "../interfaces/IMetadataMaintenanceLookup";
import {Alert, Button, Col, Form, FormGroup, Row} from "reactstrap";
import {FormField} from "./FormField";
import {WriteService} from "../services/WriteService";
import {v4 as uuid4} from "uuid";
import {MetadataEntityType} from "../enums/MetadataEntityType";

export const AddMaintenanceLookup: React.FC<{addMaintenanceLookupCallback: (ml: IMetadataMaintenanceLookup) => void}> = ({addMaintenanceLookupCallback}) => {
	const [isOpen, setIsOpen] = useState(false);
	const [errorMessageVisible, setErrorMessageVisible] = useState(false);
	
	const [pressure, setPressure] = useState("");
	const [machineTemperature, setMachineTemperature] = useState("");
	const [maintenanceAdjustmentRequired, setMaintenanceAdjustmentRequired] = useState("");
	
	const resetForm = () => {
		setPressure("");
		setMachineTemperature("");
		setMaintenanceAdjustmentRequired("");
	}
	
	const handleSubmit = (event?: React.FormEvent<HTMLFormElement>): void => {
		setErrorMessageVisible(false);
		
		const formValues: IMetadataMaintenanceLookup = {
			id: uuid4(),
			entityType: MetadataEntityType.MaintenanceLookup,
			machineTemperature,
			maintenanceAdjustmentRequired,
			pressure,
		};
		
		WriteService.createMaintenanceLookupAsync(formValues)
			.then(() => {
				setIsOpen(false);
				addMaintenanceLookupCallback(formValues);
				resetForm();
			})
			.catch(e => {
				setErrorMessageVisible(true);
				console.error(e);
			});
		
		event?.preventDefault();
	}
	
	return (
		<div>
			{isOpen
			? <Form onSubmit={handleSubmit}>
					<FormField name="Pressure" value={pressure} setValue={setPressure} />
					<FormField name="Machine Temperature" value={machineTemperature} setValue={setMachineTemperature} />
					<FormField name="Maintenance Adjustment Required" value={maintenanceAdjustmentRequired} setValue={setMaintenanceAdjustmentRequired} />

					{errorMessageVisible
						? <Row>
							<Col sm={{size: 6, offset: 2}}>
								<Alert color="danger">Something went wrong.</Alert>
							</Col>
						</Row>
						: null}

					<FormGroup row>
						<Col sm={{size: 2, offset: 2}}>
							<Button color="primary" onClick={() => handleSubmit()}>Submit</Button>
							<Button onClick={() => setIsOpen(false)} style={{marginLeft: "6px"}}>Close</Button>
						</Col>
					</FormGroup>
					
				</Form>
			: <Button color="primary" onClick={() => setIsOpen(!isOpen)} style={{marginBottom: "6px"}}>Add Machine</Button>}
		</div>
	)
}