#include <stdio.h>
#include <memory>
#include "WindowsBluetoothConnector.h"
#include "CommandSerializer.h"
#include "BluetoothWrapper.h"

void BluetoothWrapper::refresh() noexcept
{
	this->disconnect();
	Sleep(500);
	this->connect(_addr);
	Sleep(500);
}

int main()
{
	std::cout << "Initializing... If can't see the GUI, something has gone wrong." << std::endl;
	try
	{
		std::unique_ptr<IBluetoothConnector> connector = std::make_unique<WindowsBluetoothConnector>();
		BluetoothWrapper wrap(std::move(connector));
	}
	catch (const std::exception& e)
	{
	}
}

