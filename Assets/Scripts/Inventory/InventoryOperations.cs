using System;

using Item.Instance;

namespace Inventory
{
    /// <summary>
    /// The outcome of an inventory operation.
    /// </summary>
    public enum InventoryOperationStatus
    {
        Success,
        Partial,
        NoChange,
        NoSpace,
        EmptySource,
        IncompatibleTarget,
        InvalidArgument
    }

    /// <summary>
    /// Result returned to the UI after an inventory operation.
    /// MovedCount is the number of items transferred by the operation.
    /// </summary>
    public readonly struct InventoryOperationResult
    {
        public InventoryOperationStatus Status { get; }
        public int MovedCount { get; }
        public bool Succeeded =>
            Status == InventoryOperationStatus.Success ||
            Status == InventoryOperationStatus.Partial ||
            Status == InventoryOperationStatus.NoChange;

        public InventoryOperationResult(InventoryOperationStatus status, int movedCount = 0)
        {
            Status = status;
            MovedCount = movedCount;
        }
    }

    /// <summary>
    /// UI-callable operations for inventories represented by ItemInstance arrays.
    /// This class does not own inventory data; callers provide the source and target arrays.
    /// </summary>
    public static class InventoryOperations
    {
        /// <summary>
        /// Moves an item to an empty slot, or stacks it onto an item with the same ID.
        /// A partial stack transfer is allowed.
        /// </summary>
        public static InventoryOperationResult TryMove(
            ItemInstance[] source,
            int sourceIndex,
            ItemInstance[] destination,
            int destinationIndex)
        {
            if (!IsValidSlot(source, sourceIndex) ||
                !IsValidSlot(destination, destinationIndex))
                return new InventoryOperationResult(InventoryOperationStatus.InvalidArgument);

            if (ReferenceEquals(source, destination) && sourceIndex == destinationIndex)
                return new InventoryOperationResult(InventoryOperationStatus.NoChange);

            ItemInstance sourceItem = source[sourceIndex];
            if (sourceItem == null)
                return new InventoryOperationResult(InventoryOperationStatus.EmptySource);

            ItemInstance destinationItem = destination[destinationIndex];
            if (destinationItem == null)
            {
                destination[destinationIndex] = sourceItem;
                source[sourceIndex] = null;
                return new InventoryOperationResult(
                    InventoryOperationStatus.Success, sourceItem.StackCount);
            }

            if (!CanStack(sourceItem, destinationItem))
                return new InventoryOperationResult(InventoryOperationStatus.IncompatibleTarget);

            int capacity = destinationItem.Definition.MaxStackSize - destinationItem.StackCount;
            if (capacity <= 0)
                return new InventoryOperationResult(InventoryOperationStatus.NoSpace);

            int movedCount = Math.Min(sourceItem.StackCount, capacity);
            destinationItem.ChangeStackCount(destinationItem.StackCount + movedCount);

            int remainingCount = sourceItem.StackCount - movedCount;
            if (remainingCount == 0)
                source[sourceIndex] = null;
            else
                sourceItem.ChangeStackCount(remainingCount);

            InventoryOperationStatus status = remainingCount == 0
                ? InventoryOperationStatus.Success
                : InventoryOperationStatus.Partial;

            return new InventoryOperationResult(status, movedCount);
        }

        /// <summary>
        /// Swaps two slots. Empty slots are valid, so this also supports moving an item
        /// into an empty slot without stacking.
        /// </summary>
        public static InventoryOperationResult TrySwap(
            ItemInstance[] first,
            int firstIndex,
            ItemInstance[] second,
            int secondIndex)
        {
            if (!IsValidSlot(first, firstIndex) || !IsValidSlot(second, secondIndex))
                return new InventoryOperationResult(InventoryOperationStatus.InvalidArgument);

            if (ReferenceEquals(first, second) && firstIndex == secondIndex)
                return new InventoryOperationResult(InventoryOperationStatus.NoChange);

            ItemInstance temporary = first[firstIndex];
            first[firstIndex] = second[secondIndex];
            second[secondIndex] = temporary;

            return new InventoryOperationResult(InventoryOperationStatus.Success);
        }

        /// <summary>
        /// Quick-moves as many items as possible to another inventory.
        /// Matching stacks are filled first, then the first available empty slot.
        /// Any remainder stays in the source slot.
        /// </summary>
        public static InventoryOperationResult TryQuickMove(
            ItemInstance[] source,
            int sourceIndex,
            ItemInstance[] destination)
        {
            if (!IsValidSlot(source, sourceIndex) || destination == null || destination.Length == 0)
                return new InventoryOperationResult(InventoryOperationStatus.InvalidArgument);

            ItemInstance sourceItem = source[sourceIndex];
            if (sourceItem == null)
                return new InventoryOperationResult(InventoryOperationStatus.EmptySource);

            int originalCount = sourceItem.StackCount;
            int remainingCount = originalCount;
            int maxStackSize = sourceItem.Definition.MaxStackSize;

            if (maxStackSize < 1)
                return new InventoryOperationResult(InventoryOperationStatus.InvalidArgument);

            // Fill existing compatible stacks first.
            for (int i = 0; i < destination.Length && remainingCount > 0; i++)
            {
                if (ReferenceEquals(source, destination) && i == sourceIndex)
                    continue;

                ItemInstance targetItem = destination[i];
                if (targetItem == null || !CanStack(sourceItem, targetItem))
                    continue;

                int capacity = targetItem.Definition.MaxStackSize - targetItem.StackCount;
                if (capacity <= 0)
                    continue;

                int movedCount = Math.Min(remainingCount, capacity);
                targetItem.ChangeStackCount(targetItem.StackCount + movedCount);
                remainingCount -= movedCount;
            }

            // Put the remainder into the first empty slot, if one exists.
            if (remainingCount > 0)
            {
                for (int i = 0; i < destination.Length; i++)
                {
                    if (ReferenceEquals(source, destination) && i == sourceIndex)
                        continue;

                    if (destination[i] != null)
                        continue;

                    if (remainingCount == originalCount)
                    {
                        // No stack was filled: transfer the original instance.
                        destination[i] = sourceItem;
                    }
                    else
                    {
                        // A stack was filled: create a separate stack for the remainder.
                        // ItemData is the shared definition; runtime state is represented
                        // by the new ItemInstance.
                        ItemInstance splitStack = sourceItem.Definition.CreateInstance();
                        splitStack.ChangeStackCount(remainingCount);
                        destination[i] = splitStack;
                    }

                    remainingCount = 0;
                    break;
                }
            }

            int movedTotal = originalCount - remainingCount;
            if (movedTotal == 0)
                return new InventoryOperationResult(InventoryOperationStatus.NoSpace);

            if (remainingCount == 0)
                source[sourceIndex] = null;
            else
                sourceItem.ChangeStackCount(remainingCount);

            InventoryOperationStatus status = remainingCount == 0
                ? InventoryOperationStatus.Success
                : InventoryOperationStatus.Partial;

            return new InventoryOperationResult(status, movedTotal);
        }

        private static bool IsValidSlot(ItemInstance[] inventory, int index)
        {
            return inventory != null && index >= 0 && index < inventory.Length;
        }

        private static bool CanStack(ItemInstance source, ItemInstance destination)
        {
            return source != null &&
                   destination != null &&
                   source.ID == destination.ID &&
                   source.Definition.MaxStackSize > 1 &&
                   destination.Definition.MaxStackSize > 1;
        }
    }
}
